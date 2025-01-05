from fastapi import FastAPI, UploadFile, HTTPException
from markitdown import MarkItDown, UnsupportedFormatException, FileConversionException

from app.configuration.configuration import Configuration
from app.configuration.environment_variables import EnvironmentVariables

app = FastAPI(swagger_ui_parameters={})
configuration = Configuration(EnvironmentVariables())

@app.get("/ping", summary="Health check")
async def health_check():
    return {"status": "ok"}

@app.post("/read", summary="Convert document into plain text")
async def create_upload_file(file: UploadFile):
    md = MarkItDown()

    if file.size > configuration.max_file_size:
        raise HTTPException(status_code=413, detail="File too large")

    try:
        result = md.convert_stream(file.file)
        return {
            "filename": file.filename,
            "content": result,
        }
    except UnsupportedFormatException as e:
        raise HTTPException(status_code=415, detail=str(e))
    except FileConversionException as e:
        raise HTTPException(status_code=400, detail=str(e))