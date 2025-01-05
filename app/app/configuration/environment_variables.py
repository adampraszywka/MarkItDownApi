import os

from app.configuration.variables import Variables

class EnvironmentVariables(Variables):
    def get(self, name: str, default=None) -> str | None:
        return os.environ.get(name, default)