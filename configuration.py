import os

DEFAULT_MAX_FILE_SIZE_MB = 25
DEFAULT_MAX_FILE_SIZE = 1024 * 1024 * DEFAULT_MAX_FILE_SIZE_MB

class Configuration:
    def __init__(self):
        max_file_size = os.environ.get('MAX_FILE_SIZE', DEFAULT_MAX_FILE_SIZE)
        if not isinstance(max_file_size, int):
            raise ValueError('Invalid value for MAX_FILE_SIZE environment variable')

        self.max_file_size = max_file_size

    def get_max_file_size(self) -> int:
        return self.max_file_size