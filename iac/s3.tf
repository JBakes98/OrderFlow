resource "aws_s3_bucket" "orderflow_bulk_processing_bucket" {
  bucket = "orderflow-bulk-processing-bucket"

  tags = {
    Name        = "Orderflow Bucket"
    Environment = "Local"
  }
}