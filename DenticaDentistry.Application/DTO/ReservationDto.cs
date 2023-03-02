﻿namespace Dentica_Dentistry.Application.DTO;

public class ReservationDto
{
    public Guid ReservationId { get; set; }
    public int DentistIndustryId { get; set; }
    public string BookerName { get; set; }
    public DateTime ReservationDate { get; set; }
}