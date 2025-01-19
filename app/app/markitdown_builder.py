from markitdown import MarkItDown
from openai import OpenAI

from app.configuration.configuration import Configuration
from app.configuration.openai_configuration import OpenAIConfiguration


class MarkItDownBuilder:

    def __init__(self, configuration: Configuration):
        self.configuration = configuration

    def get(self) -> MarkItDown:
        openai = self._build_openai_client(self.configuration.openai_configuration)
        model = self._get_model(self.configuration)

        return MarkItDown(llm_client=openai, llm_model=model)

    @staticmethod
    def _get_model(configuration):
        return configuration.openai_configuration.model if configuration.openai_configuration is not None else None

    @staticmethod
    def _build_openai_client(openai_configuration: OpenAIConfiguration | None) -> OpenAI | None:
        if openai_configuration is None:
            return None

        return OpenAI(
            api_key=openai_configuration.api_key,
            organization=openai_configuration.organization,
            project=openai_configuration.project,
            base_url=openai_configuration.base_url
        )
