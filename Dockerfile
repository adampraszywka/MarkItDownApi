# Use an official Python runtime as the base image
FROM python:3.13-slim

# Set environment variables to prevent Python from writing .pyc files and buffering stdout/stderr
ENV PYTHONDONTWRITEBYTECODE=1
ENV PYTHONUNBUFFERED=1

# Set the working directory in the container
WORKDIR /app

# Install system dependencies
#RUN apt update && apt-get install -y \build-essential \libpq-dev \curl \curl&& rm -rf /var/lib/apt/lists/*

# Install pipenv
RUN pip install --no-cache-dir pipenv

COPY Pipfile Pipfile.lock /app/
RUN pipenv install --system --deploy
COPY . /app
EXPOSE 80

CMD ["fastapi", "run", "main.py", "--port", "80"]