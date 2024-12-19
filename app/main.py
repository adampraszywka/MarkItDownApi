from fastapi import FastAPI, UploadFile, HTTPException
from markitdown import MarkItDown, UnsupportedFormatException, FileConversionException

from configuration import Configuration

app = FastAPI()
configuration = Configuration()

@app.post("/read")
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