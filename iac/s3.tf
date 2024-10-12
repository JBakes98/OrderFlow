resource "aws_s3_bucket" "orderflow_history_bucket" {
  bucket = "orderflow-history-bucket"

  tags = {
    Name        = "OrderFlow Bucket"
    Environment = "Local"
  }
}

/*resource "aws_s3_bucket_notification" "orderflow_history_bucket_notification" {
  bucket = aws_s3_bucket.orderflow_history_bucket.id
  lambda_function {
    lambda_function_arn = module.orderflow_history_function.lambda_function_arn
    events              = ["s3:ObjectCreated:*"]
  }

  topic {
    events    = ["s3:ObjectCreated:*"]
    topic_arn = aws_sns_topic.orderflow_history_topic.arn
  }
}*/