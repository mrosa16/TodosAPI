﻿namespace TodosAPI.Application.UseCases.GetUserTask
{
    public class GetTaskRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
