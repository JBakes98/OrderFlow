resource "aws_s3_bucket" "orderflow_history_bucket" {
  bucket = "orderflow-history-bucket"

  tags = {
    Name        = "OrderFlow Bucket"
    Environment = "Local"
  }
}