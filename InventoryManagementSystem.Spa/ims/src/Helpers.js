const Helpers = {
    getBase64(file, callback, errorCallback) {
        var reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => {
          callback(reader.result);
        };
        reader.onerror = (error) => {
          errorCallback(error);
        };
     }
}

export default Helpers;