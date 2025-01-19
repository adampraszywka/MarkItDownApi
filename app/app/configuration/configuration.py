from app.configuration.openai_configuration import OpenAIConfiguration
from app.configuration.variables import Variables

DEFAULT_MAX_FILE_SIZE_MB = 25
DEFAULT_MAX_FILE_SIZE = 1024 * 1024 * DEFAULT_MAX_FILE_SIZE_MB

class Configuration:
    def __init__(self, variables: Variables):
        max_file_size_raw = variables.get('MAX_FILE_SIZE', str(DEFAULT_MAX_FILE_SIZE))
        try:
            self._max_file_size = int(max_file_size_raw)
        except ValueError:
            raise ValueError('Invalid value for MAX_FILE_SIZE environment variable')

        self._openai_api_key = variables.get('OPENAI_API_KEY')
        self._openai_model = variables.get('OPENAI_MODEL')
        self._openai_organization = variables.get('OPENAI_ORG_ID')
        self._openai_project = variables.get('OPENAI_PROJECT_ID')
        self._openai_base_url = variables.get('OPENAI_BASE_URL')

    @property
    def max_file_size(self) -> int:
        return self._max_file_size

    @property
    def openai_configuration(self) -> OpenAIConfiguration|None:
        if self._openai_api_key is None or self._openai_model is None:
            return None

        return OpenAIConfiguration(
            api_key=self._openai_api_key,
            model=self._openai_model,
            organization=self._openai_organization,
            project=self._openai_project,
            base_url=self._openai_base_url
        )

