import pytest

from app.configuration.configuration import Configuration
from app.configuration.variables import Variables

class TestVariables(Variables):
    def __init__(self, values: dict[str, str]):
        self.values = values

    def get(self, name: str, default=None) -> str | None:
        return self.values.get(name, default)

def test_max_file_size_valid():
    cfg = Configuration(TestVariables({'MAX_FILE_SIZE': '1000000'}))
    assert cfg.max_file_size == 1000000

def test_max_file_size_invalid():
    with pytest.raises(ValueError):
        Configuration(TestVariables({'MAX_FILE_SIZE': 'invalid'}))

def test_max_file_size_default():
    cfg = Configuration(TestVariables({}))
    assert cfg.max_file_size == 26214400
