resource "aws_dynamodb_table" "order_table" {
  name           = "Order"
  billing_mode   = "PROVISIONED"
  hash_key       = "Id"
  read_capacity  = 5 # Adjust according to your read/write throughput requirements
  write_capacity = 5

  stream_enabled   = true
  stream_view_type = "NEW_IMAGE"

  attribute {
    name = "Id"
    type = "S"
  }

  attribute {
    name = "InstrumentId"
    type = "S"
  }

  attribute {
    name = "Price"
    type = "N"
  }

  # Secondary index definition
  global_secondary_index {
    name            = "InstrumentPriceGSI"
    hash_key        = "InstrumentId"
    projection_type = "ALL"
    read_capacity   = 5
    write_capacity  = 5

    # Sort key definition for the GSI
    range_key = "Price"

    # Define the attributes projected into the index
    non_key_attributes = [] # Add attribute names here if projection_type is "INCLUDE"
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
