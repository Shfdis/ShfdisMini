class TokenHandler {
    isActive: boolean
    Token: string|null
    Update(): void {
        this.Token = localStorage.getItem('token');
        if (this.Token === null) { 
            this.isActive = false;
            return;
        }
        fetch(import.meta.env.API_URL + '/session/' + this.Token)
        .then((res: Response) => res.json())
        .then((res: any) => {
            if (res.status === 'success') {
                this.isActive = true;
                return;
            }
            this.isActive = false;
        })
        .catch((ex: Error) => {
            this.isActive = false;
            console.log('Something went wrong: ' + ex.message);
        });
    }
}

export default TokenHandler;