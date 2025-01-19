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

def test_openai_configuration():
    cfg = Configuration(TestVariables({
        'OPENAI_API_KEY': 'api_key',
        'OPENAI_MODEL': 'model',
        'OPENAI_ORG_ID': 'organization',
        'OPENAI_PROJECT_ID': 'project',
        'OPENAI_BASE_URL': 'base_url'
    }))

    assert cfg.openai_configuration.api_key == 'api_key'
    assert cfg.openai_configuration.model == 'model'
    assert cfg.openai_configuration.organization == 'organization'
    assert cfg.openai_configuration.project == 'project'
    assert cfg.openai_configuration.base_url == 'base_url'

def test_openai_not_configured():
    cfg = Configuration(TestVariables({}))
    assert cfg.openai_configuration is None

def test_openai_missing_api_key():
    cfg = Configuration(TestVariables({
        'OPENAI_MODEL': 'model',
        'OPENAI_ORG_ID': 'organization',
        'OPENAI_PROJECT_ID': 'project',
        'OPENAI_BASE_URL': 'base_url'
    }))
    assert cfg.openai_configuration is None

def test_openai_only_api_key_and_model():
    cfg = Configuration(TestVariables({
        'OPENAI_API_KEY': 'api_key',
        'OPENAI_MODEL': 'model'
    }))

    assert cfg.openai_configuration.api_key == 'api_key'
    assert cfg.openai_configuration.model == 'model'


def test_openai_only_api_key():
    cfg = Configuration(TestVariables({
        'OPENAI_API_KEY': 'api_key',
    }))

    assert cfg.openai_configuration is None

def test_openai_only_model():
    cfg = Configuration(TestVariables({
        'OPENAI_MODEL': 'model',
    }))

    assert cfg.openai_configuration is None