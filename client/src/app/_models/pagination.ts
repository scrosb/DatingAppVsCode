//Give our pagination info some typing
export interface Pagination {
    currentPage: number;
    itemsPerPage: number;
    totalItems: number;
    totalPages:number;
}

//Create a class
//t is an array of members
export class PaginatedResult<T>{
    result: T;
    pagination: Pagination;
}