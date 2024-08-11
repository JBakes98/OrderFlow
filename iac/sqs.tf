resource "aws_sqs_queue" "orderflow_events_queue" {
  name = "orderflow-events-queue"
  max_message_size = 2048
  message_retention_seconds = 86400
}

resource "aws_sqs_queue" "orderflow_events_queue_deadletter" {
  name = "orderflow-events-queue-deadletter"
}

resource "aws_sqs_queue" "orderflow_history_queue" {
  name = "orderflow-history-queue"
  max_message_size = 2048
  message_retention_seconds = 86400
}

resource "aws_sqs_queue" "orderflow_history_queue_deadletter" {
  name = "orderflow-history-queue-deadletter"
}

resource "aws_sns_topic_subscription" "orderflow_history" {
  endpoint  = aws_sqs_queue.orderflow_history_queue.arn
  protocol  = "sqs"
  topic_arn = aws_sns_topic.orderflow_history_topic.arn
}