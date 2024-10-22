module "orderflow_history_function" {
  source = "terraform-aws-modules/lambda/aws"

  function_name = "orderflow-history-function"
  description   = "Lambda function to process order file from s3"
  handler       = "OrderFlow.History.Processor::OrderFlow.History.Processor.Function::FunctionHandler"
  runtime       = "dotnet8"

  create_package         = false
  local_existing_package = "../src/OrderFlow.History.Processor/bin/Release/net8.0/OrderFlow.History.Processor.zip"
}

resource "aws_lambda_permission" "orderflow_history_function_permission" {
  statement_id  = "AllowS3Invoke"
  action        = "lambda:InvokeFunction"
  function_name = module.orderflow_history_function.lambda_function_name
  principal     = "s3.amazonaws.com"
  source_arn    = "arn:aws:s3:::${aws_s3_bucket.orderflow_history_bucket.id}"
}

module "orderflow_events_publisher_function" {
  source = "terraform-aws-modules/lambda/aws"

  function_name = "orderflow-events-publisher"
  description   = "Lambda function that processes events Dynamo stream and publishes them to SNS"
  runtime       = "dotnet8"
  create_package = false
  
  image_uri = "localhost:4566/orderflow-events-publisher:latest"
  package_type = "Image"
}

resource "aws_lambda_event_source_mapping" "orderflow_events_stream" {
  function_name = module.orderflow_events_publisher_function.lambda_function_name
  event_source_arn = aws_dynamodb_table.events_table.stream_arn
  starting_position = "LATEST"
}