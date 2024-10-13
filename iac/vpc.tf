/*resource "aws_vpc" "orderflow_api_vpc" {
  cidr_block = "10.0.0.0/16"
  enable_dns_support   = true
  enable_dns_hostnames = true

  tags = {
    Name = "orderflow-api-vpc"
  }
}

# Create a public subnet
resource "aws_subnet" "orderflow_api_public_subnet" {
  vpc_id            = aws_vpc.orderflow_api_vpc.id
  cidr_block        = "10.0.1.0/24"
  map_public_ip_on_launch = true
  availability_zone = "eu-west-2a" # Use one AZ to keep it simple and cheap

  tags = {
    Name = "orderflow-api-public-subnet"
  }
}

# Create an Internet Gateway
resource "aws_internet_gateway" "orderflow_api_igw" {
  vpc_id = aws_vpc.orderflow_api_vpc.id

  tags = {
    Name = "orderflow-api-igw"
  }
}

# Create a route table for the public subnet
resource "aws_route_table" "orderflow_api_public_rt" {
  vpc_id = aws_vpc.orderflow_api_vpc.id

  tags = {
    Name = "orderflow-api-public-route-table"
  }
}

# Add a route to the Internet Gateway in the public route table
resource "aws_route" "orderflow_api_public_internet_route" {
  route_table_id         = aws_route_table.orderflow_api_public_rt.id
  destination_cidr_block = "0.0.0.0/0"
  gateway_id             = aws_internet_gateway.orderflow_api_igw.id
}

# Associate the public route table with the public subnet
resource "aws_route_table_association" "orderflow_api_public_association" {
  subnet_id      = aws_subnet.orderflow_api_public_subnet.id
  route_table_id = aws_route_table.orderflow_api_public_rt.id
}

# Output the VPC and Subnet IDs for reference
output "vpc_id" {
  value = aws_vpc.orderflow_api_vpc.id
}

output "subnet_id" {
  value = aws_subnet.orderflow_api_public_subnet.id
}*/