/*resource "aws_iam_role" "ecs_task_execution_role" {
  name = "ecsTaskExecutionRole"
  assume_role_policy = jsonencode({
    Version = "2012-10-17",
    Statement = [
      {
        Action    = "sts:AssumeRole",
        Effect    = "Allow",
        Principal = {
          Service = "ecs-tasks.amazonaws.com"
        }
      }
    ]
  })

  managed_policy_arns = [
    "arn:aws:iam::aws:policy/service-role/AmazonECSTaskExecutionRolePolicy"
  ]
}

resource "aws_ecs_task_definition" "orderflow_api_task" {
  family                   = "orderflow-api-fargate-task"
  network_mode             = "awsvpc"
  requires_compatibilities = ["FARGATE"]
  cpu                      = "256"     # Cheap option: 0.25 vCPU
  memory                   = "512"     # Cheap option: 0.5 GB memory

  execution_role_arn = aws_iam_role.ecs_task_execution_role.arn

  container_definitions = jsonencode([{
    name      = "orderflow-api-container"
    image     = "${aws_ecr_repository.orderflow_api_repo.repository_url}:latest"  # Use latest image from ECR
    cpu       = 256
    memory    = 512
    essential = true
    portMappings = [{
      containerPort = 80
      hostPort      = 80
      protocol      = "tcp"
    }]
  }])
}

# ECS Cluster (assuming it's already created)
resource "aws_ecs_cluster" "orderflow_api_cluster" {
  name = "orderflow-api-cluster"
}

# Security Group for ECS
resource "aws_security_group" "orderflow_api_sg" {
  name        = "orderflow-api-sg"
  description = "Allow inbound traffic for ECS service"
  vpc_id      = aws_vpc.orderflow_api_vpc.id  

  ingress {
    from_port   = 80
    to_port     = 80
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
}

# ECS Service
resource "aws_ecs_service" "orderflow_api_service" {
  name            = "orderlflow-api-service"
  cluster         = aws_ecs_cluster.orderflow_api_cluster.id
  task_definition = aws_ecs_task_definition.orderflow_api_task.arn
  desired_count   = 1
  launch_type     = "FARGATE"

  network_configuration {
    subnets         = [aws_subnet.orderflow_api_public_subnet.id]
    security_groups = [aws_security_group.orderflow_api_sg.id]
    assign_public_ip = true
  }

  load_balancer {
    target_group_arn = aws_lb_target_group.orderflow_api_group.arn  # Optional if using a Load Balancer
    container_name   = "orderflow-api-container"
    container_port   = 80
  }

  depends_on = [
    aws_ecs_task_definition.orderflow_api_task,
  ]
}

# (Optional) Application Load Balancer (ALB)
resource "aws_lb" "orderflow_api_lb" {
  name               = "orderflow-api-lb"
  internal           = false
  load_balancer_type = "application"
  security_groups    = [aws_security_group.orderflow_api_sg.id]
  subnets            = [aws_subnet.orderflow_api_public_subnet.id]
}

# (Optional) Target Group for ECS Service
resource "aws_lb_target_group" "orderflow_api_group" {
  name        = "orderflow-api-targets"
  port        = 80
  protocol    = "HTTP"
  vpc_id      = aws_vpc.orderflow_api_vpc.id
  target_type = "ip"
}

# (Optional) Listener for ALB
resource "aws_lb_listener" "orderflow_api_lb_listener" {
  load_balancer_arn = aws_lb.orderflow_api_lb.arn
  port              = 80
  protocol          = "HTTP"

  default_action {
    type             = "forward"
    target_group_arn = aws_lb_target_group.orderflow_api_group.arn
  }
}*/