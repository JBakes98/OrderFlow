resource "aws_db_instance" "orderflow_postgres_instance" {
  allocated_storage    = 20                    # Storage size in GB
  engine               = "postgres"            # Database engine
  engine_version       = "13.4"                # PostgreSQL version
  instance_class       = "db.t3.micro"         # Instance type
  identifier           = "orderflow-postgres-instance" # Unique name for the instance
  username             = "admin"               # DB master username
  password             = "supersecretpassword" # DB master password
  db_name              = "orderflow"            # Initial database name
  publicly_accessible  = false                 # Set to true if you want public access
  skip_final_snapshot  = true                  # Skips the final snapshot upon deletion

  # VPC security group settings
  vpc_security_group_ids = [aws_security_group.db_sg.id]

  # DB subnet group settings
  db_subnet_group_name = aws_db_subnet_group.db_subnet_group.name

  # Backup settings
  backup_retention_period = 7  # Retain backups for 7 days
  backup_window           = "07:00-09:00"  # Backup window time

  # Maintenance settings
  maintenance_window = "Mon:00:00-Mon:03:00" # Maintenance window time

  # Encryption
  storage_encrypted   = true

  # Monitoring settings (optional)
  monitoring_interval = 60  # CloudWatch enhanced monitoring interval (seconds)
}

# Create security group for the RDS instance
resource "aws_security_group" "db_sg" {
  name        = "rds-postgres-sg"
  description = "Allow access to PostgreSQL"

  ingress {
    from_port   = 5432  # PostgreSQL port
    to_port     = 5432
    protocol    = "tcp"
    cidr_blocks = ["10.0.0.0/16"] # Modify this to the CIDR of your VPC
  }

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
}

# Create a DB subnet group
resource "aws_db_subnet_group" "db_subnet_group" {
  name       = "my-postgres-subnet-group"
  subnet_ids = [aws_subnet.private_subnet_az1.id, aws_subnet.private_subnet_az2.id]

  tags = {
    Name = "PostgreSQL Subnet Group"
  }
}