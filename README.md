# Orderflow

Orderflow is a personal project designed for experimenting with various technology offerings to understand their capabilities.

## Technologies Used

- **Dotnet**: The project is written in Dotnet version 8.
- **Terraform**: Infrastructure as Code (IAC) is managed using Terraform.
- **AWS**: The primary cloud provider, utilizing the following services:
  - DynamoDB
  - Kinesis Streams
  - Lambda
  - S3
  - Cognito
- **LocalStack**: Testing of AWS services is facilitated locally via TfLocal.

## Project Focus

The main focus of this project is on leveraging AWS services to build and test cloud-based solutions. The aim is to understand the integration and functionality of AWS components within a Dotnet application environment.

## Getting Started

1. **Clone the repository**:
   ```sh
   git clone https://github.com/your-username/OrderFlow.git
   ```

2. **Install dependencies**:
   - Ensure you have Dotnet 8 installed.
   - Install Terraform.
   - Set up LocalStack and TfLocal for local testing.

3. **Provision Infrastructure**:
   - Use Terraform scripts to set up the required AWS infrastructure.
   ```sh
   terraform init
   terraform apply
   ```

4. **Run the Application**:
   - Start the application using Dotnet.
   ```sh
   dotnet run
   ```

5. **Testing**:
   - Use TfLocal to test AWS services locally with LocalStack.
   ```sh
   tflocal apply
   ```

## Contributing

This project is currently a personal experimentation tool, but contributions and suggestions are welcome. Feel free to fork the repository and submit pull requests.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

---

For any questions or issues, please open an issue on the repository or contact the maintainer directly.
