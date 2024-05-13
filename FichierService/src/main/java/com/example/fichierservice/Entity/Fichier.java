package com.example.fichierservice.Entity;
import jakarta.persistence.*;
import lombok.Data;
import java.time.LocalDateTime;
@Data
@Entity
@Table
public class Fichier {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id")
    private long id ;
    private String MyPath; // chemin de Fichier
    private String IdConversation ;
    private String Emeteur ;
    private String Recepteur ;
    @Column(name = "date")
    private LocalDateTime date;
    private String nomFichier ;
}