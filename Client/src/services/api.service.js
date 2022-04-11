import { httpService } from "./http.service";

export const apiService = {
    get
}

async function get(itemsPerPage, from) {
    return await httpService.get('data/page', {itemsPerPage, from});
}