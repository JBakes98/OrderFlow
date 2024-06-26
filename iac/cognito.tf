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

  email_configuration {
    email_sending_account = "COGNITO_DEFAULT"
  }
}

resource "aws_cognito_user_pool_client" "client" {
  name         = "orderflow-client"
  user_pool_id = aws_cognito_user_pool.user_pool.id

  generate_secret     = false
  explicit_auth_flows = ["ALLOW_USER_PASSWORD_AUTH", "ALLOW_REFRESH_TOKEN_AUTH"]

  # Add OAuth settings
  allowed_oauth_flows = ["code", "implicit"]
  allowed_oauth_scopes = [
    "email",
    "openid",
    "profile",
    "${aws_cognito_resource_server.resource_server.identifier}/read:data",
    "${aws_cognito_resource_server.resource_server.identifier}/write:data"
  ]
  allowed_oauth_flows_user_pool_client = true
  supported_identity_providers         = ["COGNITO"]

  callback_urls = ["https://google.com"]
  logout_urls   = ["https://bbc.com"]
}

resource "aws_cognito_user_pool_domain" "cognito-domain" {
  domain       = "orderflow"
  user_pool_id = aws_cognito_user_pool.user_pool.id
}

resource "aws_cognito_resource_server" "resource_server" {
  identifier = "orderflow"
  name       = "orderflow"

  user_pool_id = aws_cognito_user_pool.user_pool.id

  scope {
    scope_description = "Read access to user data"
    scope_name        = "read:data"
  }

  scope {
    scope_description = "Write access to user data"
    scope_name        = "write:data"
  }
}