from app.configuration.variables import Variables

DEFAULT_MAX_FILE_SIZE_MB = 25
DEFAULT_MAX_FILE_SIZE = 1024 * 1024 * DEFAULT_MAX_FILE_SIZE_MB

class Configuration:
    def __init__(self, variables: Variables):
        max_file_size_raw = variables.get('MAX_FILE_SIZE', str(DEFAULT_MAX_FILE_SIZE))
        try:
            self.max_file_size = int(max_file_size_raw)
        except ValueError:
            raise ValueError('Invalid value for MAX_FILE_SIZE environment variable')

    def get_max_file_size(self) -> int:
        return self.max_file_size