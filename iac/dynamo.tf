resource "aws_dynamodb_table" "events_table" {
  name           = "OrderEvents"
  billing_mode   = "PROVISIONED"
  hash_key       = "StreamId"
  range_key      = "EventType"
  read_capacity  = 5 # Adjust according to your read/write throughput requirements
  write_capacity = 5

  stream_enabled   = true
  stream_view_type = "NEW_AND_OLD_IMAGES"

  ttl {
    attribute_name = "_deletionDate"
    enabled        = true
  }

  attribute {
    name = "StreamId"
    type = "S"
  }

  attribute {
    name = "EventType"
    type = "S"
  }
}
