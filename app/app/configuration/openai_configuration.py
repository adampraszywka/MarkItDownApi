from pydantic import BaseModel


class OpenAIConfiguration(BaseModel):
    api_key: str
    model: str
    organization: str|None
    project: str|None
    base_url: str|None