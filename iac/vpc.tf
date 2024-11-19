resource "aws_vpc" "orderflow_api_vpc" {
  cidr_block = "10.0.0.0/16"
  enable_dns_support   = true
  enable_dns_hostnames = true

  tags = {
    Name = "orderflow-vpc"
  }
}

# Create a Public Subnet in Availability Zone 1
resource "aws_subnet" "public_subnet_az1" {
  vpc_id     = aws_vpc.orderflow_api_vpc.id
  cidr_block = "10.0.1.0/24"
  availability_zone = "eu-west-2a"

  tags = {
    Name = "Public Subnet 1"
  }
}

# Create a Public Subnet in Availability Zone 2
resource "aws_subnet" "public_subnet_az2" {
  vpc_id     = aws_vpc.orderflow_api_vpc.id
  cidr_block = "10.0.2.0/24"
  availability_zone = "eu-west-2b"

  tags = {
    Name = "Public Subnet 2"
  }
}

# Create a Private Subnet in Availability Zone 1 (For RDS)
resource "aws_subnet" "private_subnet_az1" {
  vpc_id     = aws_vpc.orderflow_api_vpc.id
  cidr_block = "10.0.3.0/24"
  availability_zone = "eu-west-2a"

  tags = {
    Name = "Private Subnet 1"
  }
}

# Create a Private Subnet in Availability Zone 2 (For RDS)
resource "aws_subnet" "private_subnet_az2" {
  vpc_id     = aws_vpc.orderflow_api_vpc.id
  cidr_block = "10.0.4.0/24"
  availability_zone = "eu-west-2b"

  tags = {
    Name = "Private Subnet 2"
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
  subnet_id      = aws_subnet.private_subnet_az1.id
  route_table_id = aws_route_table.orderflow_api_public_rt.id
}

# Output the VPC and Subnet IDs for reference
output "vpc_id" {
  value = aws_vpc.orderflow_api_vpc.id
}