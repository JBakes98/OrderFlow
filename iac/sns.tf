data "aws_iam_policy_document" "orderflow_bulk_processing_topic_policy" {
  statement {
    effect = "Allow"
    principals {
      identifiers = ["s3.amazonaws.com"]
      type        = "Service"
    }
    actions   = ["SNS:Publish"]
    resources = [aws_s3_bucket.orderflow_bulk_processing_bucket.arn]
    condition {
      test     = "ArnLike"
      values   = [aws_s3_bucket.orderflow_bulk_processing_bucket.arn]
      variable = "aws:SourceArn"
    }
  }
}
resource "aws_sns_topic" "orderflow_bulk_processing_topic" {
  name   = "orderflow-s3-bulk-processing-topic"
  policy = data.aws_iam_policy_document.orderflow_bulk_processing_topic_policy.json
}