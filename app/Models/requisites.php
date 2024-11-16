<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class requisites extends Model
{
    use HasFactory;
    protected $fillable=['registration_date', 'hospital_reduce_name', 'name_legal_faces', 'ogrn','inn', 'kpp', 'hospital_id'];
}
