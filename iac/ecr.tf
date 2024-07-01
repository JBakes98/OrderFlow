resource "aws_ecr_repository" "orderflow_api_repo" {
  name                 = "orderflow-api"
  image_tag_mutability = "MUTABLE"

  image_scanning_configuration {
    scan_on_push = true
  }
}

resource "aws_ecr_repository_policy" "api_repo_policy" {
  repository = aws_ecr_repository.orderflow_api_repo.name

  policy = jsonencode({
    Version = "2012-10-17"
    Statement = [
      {
        Sid    = "AllowPushPull"
        Effect = "Allow"
        Principal = {
          AWS = "arn:aws:iam::${data.aws_caller_identity.current.account_id}:root"
        }
        Action = [
          "ecr:GetDownloadUrlForLayer",
          "ecr:BatchGetImage",
          "ecr:BatchCheckLayerAvailability",
          "ecr:PutImage",
          "ecr:InitiateLayerUpload",
          "ecr:UploadLayerPart",
          "ecr:CompleteLayerUpload"
        ]
      }
    ]
  })
}

# Lifecycle policy to keep only the latest 5 images
resource "aws_ecr_lifecycle_policy" "api_repo_lifecycle" {
  repository = aws_ecr_repository.orderflow_api_repo.name

  policy = jsonencode({
    rules = [{
      rulePriority = 1
      description  = "Keep last 5 images"
      selection = {
        tagStatus   = "any"
        countType   = "imageCountMoreThan"
        countNumber = 5
      }
      action = {
        type = "expire"
      }
    }]
  })
}


# Get current AWS account ID
data "aws_caller_identity" "current" {}

# Output the repository URL
output "repository_url" {
  value = aws_ecr_repository.orderflow_api_repo.repository_url
}