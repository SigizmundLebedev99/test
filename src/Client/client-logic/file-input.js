import handleFile from './handle-file';

function setCallback(cb){
    let input = document.getElementById('file');
    input.onchange = onFileChange(cb);
}

function onFileChange(cb){
    function fileMessage(msg){
        let span = document.getElementById('filemessage');
        span.innerText = msg;
    }
    return ($event) =>{
        const files = $event.target.files;
        if (!files.length){
            fileMessage("Файл не выбран");
            return;
        }
        $event.target.files = null;
        fileMessage("Загрузка...");
        handleFile(files[0])
        .then(json=>{
            fileMessage("Выберите файл...");
            cb(json);
        })
        .catch(error=>{
            fileMessage("Произошла ошибка. Попробуйте еще раз...");
            console.log(error);
        });
    }
}
export default setCallback;
