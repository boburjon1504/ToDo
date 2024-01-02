import ApiClientBase from "@/infrastructure/apiClients/apiClientBase/ApiClientBase";
import { ToDoItem } from "@/modules/todos/models/ToDoItem";
import type { Guid } from "guid-typescript";

export class TodoEndpointsDetails {
    private client: ApiClientBase;

    constructor(client: ApiClientBase) {
        this.client = client;
    }

    public async getAsync() {
        return await this.client.getAsync<Array<ToDoItem>>("api/todo");
    }

    public async getByIdAsync(id: Guid) {
        return await this.client.getAsync<ToDoItem>(`api/todo/${id}`);
    }

    public async createAsync(todo: ToDoItem) {
        return await this.client.postAsync<ToDoItem>("api/todo", todo);
    }

    public async updateAsync(todo: ToDoItem) {
        return await this.client.putAsync<ToDoItem>("api/todo", todo);
    }

    public async deleteByIdAsync(id: Guid) {
        return await this.client.deleteAsync<ToDoItem>(`api/todo/${id}`);
    }
}