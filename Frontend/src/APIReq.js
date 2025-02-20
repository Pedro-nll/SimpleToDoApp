import axios from "axios";

export class APIReq {
    constructor(host) {
        this.host = host;
    }

    async getRequest(path) {
        try {
            const head = {
                'Content-Type': 'application/json'
            };

            const config = {
                method: 'GET',
                headers: head
            };

            const response = await fetch(`${this.host}${path}`, config);
            if (response.ok) {
                try {
                    const data = await response.json();
                    return data;
                } catch (error) {
                    return null;
                }
            } else {
                return response.status;
            }
        } catch (error) {
            console.error('Error:', error);
        }
    }
    
    async postRequest(path, body) {
        try {
            const config = {
                headers: {
                    'Content-Type': 'application/json'
                }
            };
    
            const response = await axios.post(`${this.host}${path}`, body, config);
    
            return response;
        } catch (error) {
            if (error.response) {
                console.error(`Error: ${error.response.status} - ${error.response.statusText}`);
                return { status: error.response.status, title: error.response.statusText };
            } else if (error.request) {
                console.error('Error: No response received');
                return { status: null, title: 'No response received' };
            } else {
                console.error('Error:', error.message);
                return { status: null, title: error.message };
            }
            
        }
    }
    

    async deleteRequest(path) {
        try {
            const config = {
                headers: {
                    'Content-Type': 'application/json'
                }
            };

            const response = await axios.delete(`${this.host}${path}`, config);

            return response;
        } catch (error) {
            console.error('Error:', error);
        }
    }
}
