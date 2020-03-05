const ServiceModel = {
    id: 0,
    code: '',
    price: 0,
    description: '',
    toQueryString(){
        return `?id=${this.id}&code=${this.code}&description=${this.description}&price=${this.price}`;
    },
    assignText(val){
        return Object.keys(this)
                .reduce((p, c) => ({...p, [c]: typeof this[c] === "function"
                    || typeof this[c] === "number" ? this[c] : val }), {});
    },
    equals(obj){
        return this.id === obj.id && this.code === obj.code 
            && this.price === obj.price && this.description === obj.description;
    }
}
export default ServiceModel;