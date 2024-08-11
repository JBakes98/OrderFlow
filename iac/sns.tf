resource "aws_sns_topic" "orderflow_history_topic" {
  name = "orderflow-s3-order-history-topic"
  policy = data.aws_iam_policy_document.orderflow_history_topic_policy.json
}

data "aws_iam_policy_document" "orderflow_history_topic_policy" {
  statement {
    effect = "Allow"
    
    principals {
      identifiers = ["s3.amazonaws.com"]
      type        = "Service"
    }
    
    actions = ["SNS:Publish"]
    resources = [aws_sns_topic.orderflow_history_topic.arn]
    
    condition {
      test     = "ArnLike"
      values   = [aws_s3_bucket.orderflow_history_bucket.arn]
      variable = "aws:SourceArn"
    }
  }
}