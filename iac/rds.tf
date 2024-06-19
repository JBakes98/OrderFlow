resource "aws_db_instance" "orderflow-postgres-db" {
  allocated_storage = 20
  engine = "postgres"
  engine_version = "16.3-R1"
  identifier = "orderflow-postgres-db"
  instance_class = "db.t3.micro"
  username = "postgres"
  password = "Password1!"
  skip_final_snapshot = true
  storage_encrypted = false
  publicly_accessible = false
  apply_immediately = true
}