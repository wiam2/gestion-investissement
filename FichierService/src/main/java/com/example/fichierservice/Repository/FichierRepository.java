package com.example.fichierservice.Repository;

import com.example.fichierservice.Entity.Fichier;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Optional;

@Repository
public interface FichierRepository extends JpaRepository<Fichier , String> {

    List<Fichier> findByNomFichier(String nomFichier);
}

