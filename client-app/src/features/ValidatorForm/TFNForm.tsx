import { useState } from 'react';
import toast from 'react-hot-toast';
import { TFN } from '../../app/api/agent';
import { TaxFileNumber } from '../../app/models/TaxFileNumber';


export function TFNForm() {
  const [tfn, setTfn] = useState<string>("");
  
  const [loading, setLoading] = useState<boolean>(false);

  function submit(event: any) {
    event.preventDefault()
    setLoading(true);
    
    let taxFileNumber: TaxFileNumber = { TFN: tfn };

    TFN.validate(taxFileNumber).then((message) => {
      const { tfnIsValid, errorMessage } = message;
      setTfn("");
      if (tfnIsValid){
        toast.success(errorMessage)
      }
      else{
        toast.error(errorMessage)
      }
      setLoading(false);
    }
    ).catch((err) => {
     if (err.status === 400) {
        err?.data?.errors?.TFN.forEach( (errorMessage : string) =>{
          toast.error(errorMessage);
        })
      }
      else {
        console.log("Bad Request : " + err);
      }
      
    }).finally(()=>{
      setLoading(false);
    })

  }

  function onTfnChange(e: any) {
    setTfn(e.target.value)
  }

  return (
    <form id="tfn-form"
    onSubmit={event => {
       submit(event)
    }} noValidate>
      <div className="row justify-content-center my-5" >
        <div className="col-sm-1 my-2 mx-4 ">
          <label >Enter Tax File Number</label>
        </div>
        <div className="col-sm-4 ">
          <input type="number" className="form-control" id="inlineFormInputGroupUsername" placeholder="Enter 8 or 9 digit TFN" onChange={(e) => onTfnChange(e)} value={tfn}/>
        </div>

        <div className="col-auto mx-5">
          <button type="submit" className="btn btn-primary">{loading ? <span className="spinner-grow spinner-grow-sm mx-1" role="status" aria-hidden="true"></span> : null}Submit</button>
        </div>
      </div>
    </form>
  );
}