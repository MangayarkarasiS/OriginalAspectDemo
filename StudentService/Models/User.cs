﻿using System.ComponentModel.DataAnnotations;

namespace StudentService.Models
{
    /* public class User
     {
         [Key]
         public int Id { get; set; }
         public string Name { get; set; }
    [HttpGet("monthly-mark-differences")]
 public IActionResult GetMonthlyMarkDifferences()
 {
     var students = GetStudents(); // Replace with actual data source

     var sorted = students.OrderBy(s => s.DateOfBirth).ToList();
     var differences = new List<object>();

     for (int i = 1; i < sorted.Count; i++)
     {
         var prev = sorted[i - 1];
         var current = sorted[i];
         differences.Add(new
         {
             From = prev.Name,
             To = current.Name,
             MonthGap = current.DateOfBirth.Month - prev.DateOfBirth.Month,
             MarkDifference = current.Marks - prev.Marks
         });
     }

     return Ok(differences);
 }

     }*/
}
