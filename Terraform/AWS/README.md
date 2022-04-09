Create EC2 instance on AWS following https://learn.hashicorp.com/tutorials/terraform/aws-build?in=terraform/aws-get-started
from a Windows machine

Requirements
* Create IAM user with Key and Secret

Set your secrets locally with

`$Env:AWS_ACCESS_KEY_ID="<KEY_ID>"`

`$Env:AWS_SECRET_ACCESS_KEY="<KEY_SECRET>"`

`$Env:AWS_DEFAULT_REGION="<AWS_REGION>"`

Though the Terraform file already describes what is going to happen, this will do the following:
* Create EC2 instance on US-EAST-1 using Free-Tier AMI ami-0c02fb55956c7d316 for 64-bit and x86 architecture

Utilize through being in this working directory then
1. terraform init
2. terraform apply

Destroy instance
1. terraform apply -destroy