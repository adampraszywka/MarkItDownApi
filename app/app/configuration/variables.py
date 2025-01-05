from abc import ABC, abstractmethod


class Variables(ABC):
    @abstractmethod
    def get(self, name: str, default=None) -> str | None:
        raise NotImplementedError