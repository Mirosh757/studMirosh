<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class doctor_speciality extends Model
{
    use HasFactory;
    protected $fillable=['date_birth', 'date_admission', 'speciality_id', 'doctor_id', 'department_id'];
}
