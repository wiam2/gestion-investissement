package com.example.fichierservice.Controller;

import com.example.fichierservice.Entity.Fichier;
import com.example.fichierservice.Service.ServiceFichier;
import jakarta.servlet.annotation.MultipartConfig;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.core.io.*;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;

import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;

import org.springframework.core.io.ByteArrayResource;
import org.springframework.http.HttpHeaders;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;


import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.List;
import java.util.Optional;

@Slf4j
@RestController
@RequestMapping("/api/fichier")
@CrossOrigin(origins = {"http://localhost:4200/", "http://localhost:5055"})

@MultipartConfig
public class FichierController {

    @Autowired
    private ServiceFichier fichierService;



    @PostMapping("/upload")
    public ResponseEntity<String> uploadFiles(
            @RequestPart("MyFile") MultipartFile  MyFile,
            @RequestParam("Emeteur") String Emeteur,
            @RequestParam("Recepteur") String  Recepteur,
            @RequestParam("NomFichier") String  NomFichier
    )
    {


        try {

            log.info("{}",MyFile,Emeteur,Recepteur,NomFichier);

            // Call the service to add files
           Fichier fichier = fichierService.addFiles(MyFile,Emeteur,Recepteur,NomFichier);
            return ResponseEntity.ok("Files uploaded successfully. Fichier ID: " + fichier.getId());
        } catch (IOException e) {
            e.printStackTrace();
            return ResponseEntity.status(500).body("Error uploading files");
        }
        /* return ResponseEntity.ok("Files uploaded successfully. Fichier ID: " );*/
    }

    //download fichiers

  @GetMapping("/download/File/{fichierId}")
    public ResponseEntity<Resource> downloadMyFile(@PathVariable String fichierId) {
        return downloadFile(fichierId);

    }

    private ResponseEntity<Resource> downloadFile(String fichierId) {
        try {
            // Récupérer le fichier à partir de l'ID
            Fichier fichier = fichierService.getFichierById(fichierId);

            // Vérifier si le fichier existe
            if (fichier == null) {
                return ResponseEntity.notFound().build();
            }

            String cheminFichier =  fichier.getMyPath() ;
            byte[] fileContent = Files.readAllBytes(Paths.get(cheminFichier));

            ByteArrayResource resource = new ByteArrayResource(fileContent);

            HttpHeaders headers = new HttpHeaders();

            headers.add(HttpHeaders.CONTENT_DISPOSITION, "attachment; filename=" + "File"+ "_" + fichier.getId() + ".pdf");

            // Retourner la réponse avec le contenu du fichier
            return ResponseEntity.ok()
                    .headers(headers)
                    .contentLength(fileContent.length)
                    .contentType(MediaType.APPLICATION_PDF)
                    .body(resource);
        } catch (IOException e) {
            e.printStackTrace();
            return ResponseEntity.status(500).body(null);
        }
    }
    @GetMapping("/download/Filebyname/{NomFichier}")
    public ResponseEntity<Resource> downloadFileByNom(@PathVariable String NomFichier) {
        return downloadFileByName(NomFichier);

    }

    private ResponseEntity<Resource> downloadFileByName(String fileName) {
        try {
            // Récupérer le fichier à partir du nom de fichier
            Optional<Fichier> fichierOptional = fichierService.getFichierByName(fileName);

            // Vérifier si le fichier existe
            if (fichierOptional.isPresent()) {
                Fichier fichier = fichierOptional.get();
                String cheminFichier = fichier.getMyPath();

                // Vérifier si le fichier physique existe
                if (Files.exists(Paths.get(cheminFichier))) {
                    byte[] fileContent = Files.readAllBytes(Paths.get(cheminFichier));
                    ByteArrayResource resource = new ByteArrayResource(fileContent);

                    HttpHeaders headers = new HttpHeaders();
                    headers.add(HttpHeaders.CONTENT_DISPOSITION, "attachment; filename=" + "File" + "_" + fichier.getId() + ".pdf");

                    // Retourner la réponse avec le contenu du fichier
                    return ResponseEntity.ok()
                            .headers(headers)
                            .contentLength(fileContent.length)
                            .contentType(MediaType.APPLICATION_PDF)
                            .body(resource);
                } else {
                    // Le fichier physique n'existe pas
                    return ResponseEntity.notFound().build();
                }
            } else {
                // Le fichier n'existe pas dans la base de données
                return ResponseEntity.notFound().build();
            }
        } catch (IOException e) {
            e.printStackTrace();
            // Une erreur s'est produite lors de la lecture du fichier
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(null);
        }
    }


    // Fonction de concaténation de tableaux de bytes
    private byte[] concatArrays(byte[]... arrays) {
        int totalLength = 0;
        for (byte[] array : arrays) {
            totalLength += array.length;
        }

        byte[] result = new byte[totalLength];
        int currentIndex = 0;

        for (byte[] array : arrays) {
            System.arraycopy(array, 0, result, currentIndex, array.length);
            currentIndex += array.length;
        }

        return result;
    }
    @GetMapping("/{conversationID}/Fichiers")
    public ResponseEntity<List<Fichier>> getStagesForPost(@PathVariable String conversationID) {
        log.info("{}",conversationID);
        try {
            List<Fichier> stages = fichierService.getFichiersbyConversation(conversationID);

            if (stages.isEmpty()) {
                return ResponseEntity.notFound().build();
            }

            return ResponseEntity.ok(stages);
        } catch (Exception e) {
            return ResponseEntity.badRequest().body(null); // Gérer l'erreur de données invalides ici
        }
    }
}

