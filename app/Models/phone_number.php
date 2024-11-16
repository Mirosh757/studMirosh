<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class phone_number extends Model
{
    use HasFactory;
    protected $fillable=['contact_phone_number', 'general_page_id'];
}
