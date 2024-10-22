resource "aws_ecr_repository" "orderflow_api_repo" {
  name                 = "orderflow-api" 
  image_tag_mutability = "MUTABLE"
}

resource "aws_ecr_repository" "orderflow_events_publisher_repo" {
  name                 = "orderflow-events-publisher"
  image_tag_mutability = "MUTABLE"
}


resource "aws_ecr_repository" "orderflow_history_processor_repo" {
  name                 = "orderflow-history-processor"
  image_tag_mutability = "MUTABLE"
}