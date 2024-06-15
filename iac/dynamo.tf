resource "aws_dynamodb_table" "order_table" {
  name           = "Order"
  billing_mode   = "PROVISIONED"
  hash_key       = "Id"
  read_capacity  = 5 # Adjust according to your read/write throughput requirements
  write_capacity = 5

  attribute {
    name = "Id"
    type = "S"
  }
}

resource "aws_dynamodb_table" "events_table" {
  name           = "OrderEvents"
  billing_mode   = "PROVISIONED"
  hash_key       = "StreamId"
  range_key = "EventType"
  read_capacity  = 5 # Adjust according to your read/write throughput requirements
  write_capacity = 5

  stream_enabled   = true
  stream_view_type = "NEW_IMAGE"
  
  ttl {
    attribute_name = "_deletionDate"
    enabled = true
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

resource "aws_dynamodb_table" "instrument_table" {
  name           = "Instrument"
  billing_mode   = "PROVISIONED"
  hash_key       = "Id"
  read_capacity  = 5 # Adjust according to your read/write throughput requirements
  write_capacity = 5

  attribute {
    name = "Id"
    type = "S"
  }
}
