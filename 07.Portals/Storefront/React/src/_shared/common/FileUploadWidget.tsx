import React, { useState } from "react";
import { IUploadResult } from "../../_api/cvApi.shared";
import { useViewState } from "../customHooks/useViewState";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrash } from "@fortawesome/free-solid-svg-icons";
import scssVariables from "../../_meta/css/exposedToJSVariables.module.scss";
import cvApi from "../../_api/cvApi";
import { UploadStoredFileDto } from "../../_api/cvApi.Media";

async function uploadFile(
  body: FormData,
  setPercentage: React.Dispatch<React.SetStateAction<number>>
) {
  try {
    const options = {
      onUploadProgress: (progressEvent: any) => {
        console.log(progressEvent, " progress event");
        const { loaded, total } = progressEvent;
        const percent = Math.floor((loaded * 100) / total);

        if (percent <= 100 && setPercentage) {
          setPercentage(percent);
        }
      }
    };
    const dto = {} as UploadStoredFileDto;
    const result = await cvApi.media.UploadStoredFile(dto);
    return result.data;
  } catch (error) {
    console.log(error);
  }
}

const ProgressBar = (props: any) => {
  const { percentage } = props;
  if (!percentage) {
    return null;
  }
  return (
    <div
      className="w-100 rounded bg-light mt-3 position-relative"
      style={{ height: "5px" }}>
      <div
        style={{
          width: `${percentage}%`,
          transition: "width 1s",
          background: scssVariables.success,
          height: "100%"
        }}></div>
    </div>
  );
};

interface IFileUploadWidgetProps {
  allowMultiple?: boolean;
  limit?: number;
  acceptableFileTypes?: Array<string>;
  uploadCallback?: Function;
  uploadType?: string; // should be required
}

export const FileUploadWidget = (props: IFileUploadWidgetProps) => {
  const [percentage, setPercentage] = useState<number>(0);
  const [uploadedFiles, setUploadedFiles] = useState<Array<IUploadResult>>([]);
  const [inputKey, setInputKey] = useState<string>("");

  const { setRunning, finishRunning, viewState } = useViewState();

  async function handleFile(e: React.ChangeEvent<HTMLInputElement>) {
    try {
      setRunning();
      const file = e.target.files[0];
      const formData = new FormData();
      formData.append("image", file);
      const fileUploadResponse = await uploadFile(formData, setPercentage);
      // TODO: match FormData with IUpload and IUploadResult types
      // setUploadedFiles(fileUploadResponse.UploadFiles);
      setInputKey(Math.random().toString(36));
      setPercentage(0);
      finishRunning();
    } catch (err: any) {
      setPercentage(0);
      finishRunning(true, err);
    }
  }

  return (
    <div>
      <div className="bg-light border-1 my-2 p-3">
        <input
          key={inputKey}
          onChange={handleFile}
          type="file"
          disabled={viewState.running}
        />
        <ProgressBar percentage={percentage} />
      </div>
      {uploadedFiles.length ? (
        <ul className="bg-white border list-unstyled py-2 px-3">
          {uploadedFiles.map((file) => {
            const pathPieces = file.FileName.split("\\");
            const name = pathPieces[pathPieces.length - 1];
            return (
              <li
                className="d-flex align-items-center justify-content-between"
                key={file.FileName}>
                <span>{name}</span>
                <button className="btn" type="button" onClick={() => {}}>
                  <FontAwesomeIcon icon={faTrash} className="text-danger" />
                </button>
              </li>
            );
          })}
        </ul>
      ) : null}
    </div>
  );
};

FileUploadWidget.defaultProps = {
  allowMultiple: false,
  acceptableFileTypes: [".jpg", ".png", ".docx", ".pdf"]
};

/*
  **** Delete Process ****
  User clicks on delete button for a given file
  Update uploadedItems state to remove selected file by ID (.filter)

  The following reactions happen...
    - new <li> shows up for added item from uploadedItems
      - include icon, delete button, name of file, percentage in view for item
    - <li> removed when item deleted from uploadedItems
    - label next to input button changes ("uploading" | "done" | "")
    - input disabled/enabled based on limit
      - error message if limit reached
*/
