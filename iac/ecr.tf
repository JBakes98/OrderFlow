resource "aws_ecr_repository" "orderflow_api_repo" {
  name                 = "orderflow-api" 
  image_tag_mutability = "MUTABLE"
}