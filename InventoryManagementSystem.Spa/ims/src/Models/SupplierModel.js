const SupplierModel = {
    id: 0,
    name: '',
    email: '',
    telephone: '',
    location: '',
    toQueryString(){
        return `?id=${this.id}&name=${this.name}&email=${this.email}&telephone=${this.telephone}&location=${this.location}`;
    },
    assignText(val){
        return Object.keys(this)
                .reduce((p, c) => ({...p, [c]: typeof this[c] === "function"
                    || typeof this[c] === "number" ? this[c] : val }), {});
    },
    equals(obj){
        return this.id === obj.id && this.name === obj.name 
            && this.email === obj.email && this.telephone === obj.telephone
            && this.location === obj.location;
    }
}
export default SupplierModel;