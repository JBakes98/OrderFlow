resource "aws_cognito_user_pool" "user_pool" {
  name = "user-pool"
  
  username_attributes = ["email"]
  
  password_policy {
    minimum_length = 6
  }
  
  verification_message_template {
    default_email_option = "CONFIRM_WITH_CODE"
    email_subject = "Account Confirmation"
    email_message = "Your confirmation code is {####}"
  }
  
  schema {
    attribute_data_type = "String"
    name                = "email"
    mutable = true
    required = true
    
    string_attribute_constraints {
      min_length = 1
      max_length = 256
    }
  }
}

resource "aws_cognito_user_pool_client" "client" {
  name = "client"
  user_pool_id = aws_cognito_user_pool.user_pool.id
  
  generate_secret = true
}

resource "aws_cognito_user_pool_domain" "cognito-domain" {
  domain = "orderflow"
  user_pool_id = "${aws_cognito_user_pool.user_pool.id}"
}

resource "aws_cognito_resource_server" "resource" {
  identifier   = "orderflow"
  name         = "orderflow"
  
  user_pool_id = aws_cognito_user_pool.user_pool.id
  
  scope {
    scope_name = "sample-scope"
    scope_description        = "A Sample Scope"
  }
}