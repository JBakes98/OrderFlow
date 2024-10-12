data "aws_iam_policy_document" "orderflow_history_topic_policy" {
  statement {
    effect = "Allow"

    principals {
      identifiers = ["s3.amazonaws.com"]
      type        = "Service"
    }

    actions   = ["SNS:Publish"]
    resources = ["arn:aws:sns:*:*:orderflow-s3-order-history-topic"]

    condition {
      test     = "ArnLike"
      values   = [aws_s3_bucket.orderflow_history_bucket.arn]
      variable = "aws:SourceArn"
    }
  }
}

resource "aws_sns_topic" "orderflow_history_topic" {
  name   = "orderflow-s3-order-history-topic"
  policy = data.aws_iam_policy_document.orderflow_history_topic_policy.json
}

resource "aws_sns_topic" "orderflow_events_topic" {
  name = "orderflow-events-topic"
}