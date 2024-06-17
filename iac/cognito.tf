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

resource "aws_cognito_user_pool_domain" "cognito-domain" {
  domain = "orderflow"
  user_pool_id = "${aws_cognito_user_pool.user_pool.id}"
}