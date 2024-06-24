resource "aws_cognito_user_pool" "user_pool" {
  name                = "user-pool"
  username_attributes = ["email"]

  password_policy {
    minimum_length = 6
  }

  verification_message_template {
    default_email_option = "CONFIRM_WITH_CODE"
    email_subject        = "Account Confirmation"
    email_message        = "Your confirmation code is {####}"
  }

  schema {
    attribute_data_type = "String"
    name                = "email"
    mutable             = true
    required            = true

    string_attribute_constraints {
      min_length = 1
      max_length = 256
    }
  }
}

resource "aws_cognito_user_pool_client" "client" {
  name         = "client"
  user_pool_id = aws_cognito_user_pool.user_pool.id

  generate_secret     = true
  callback_urls       = ["https://www.bbc.co.uk"]
  explicit_auth_flows = ["ALLOW_USER_PASSWORD_AUTH", "ALLOW_REFRESH_TOKEN_AUTH"]
}

resource "aws_cognito_user_pool_client" "password-client" {
  name         = "password-client"
  user_pool_id = aws_cognito_user_pool.user_pool.id

  explicit_auth_flows          = ["ALLOW_USER_PASSWORD_AUTH", "ALLOW_REFRESH_TOKEN_AUTH"]
  callback_urls                = ["https://google.com"]
  supported_identity_providers = ["COGNITO"]
  allowed_oauth_flows          = ["implicit"]
  allowed_oauth_scopes = [
    "email",
    "openid",
    "https://orderflow.api.com/user_orders.read",
    "https://orderflow.api.com/user_orders.write",
  ]
}

resource "aws_cognito_user_pool_domain" "cognito-domain" {
  domain       = "orderflow"
  user_pool_id = aws_cognito_user_pool.user_pool.id
}

resource "aws_cognito_resource_server" "resource" {
  identifier = "orderflow"
  name       = "orderflow"

  user_pool_id = aws_cognito_user_pool.user_pool.id

  scope {
    scope_name        = "sample-scope"
    scope_description = "A Sample Scope"
  }
  
  scope {
    scope_description = "Read users orders"
    scope_name        = "https://orderflow.api.com/user_orders.read"
  }
  
  scope {
    scope_description = "Create orders"
    scope_name        = "https://orderflow.api.com/user_orders.write"
  }
}