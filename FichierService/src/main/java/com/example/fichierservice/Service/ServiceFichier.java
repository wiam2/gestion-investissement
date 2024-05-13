package com.example.fichierservice.Service;

import com.example.fichierservice.Entity.Fichier;
import com.example.fichierservice.Repository.FichierRepository;
import net.sf.jsqlparser.expression.DateTimeLiteralExpression;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.web.multipart.MultipartFile;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.StandardCopyOption;
import java.time.LocalDateTime;
import java.time.ZoneId;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;
import java.util.stream.Stream;

@Service
public class ServiceFichier {

    @Autowired
    private FichierRepository fichierRepository;

    public Fichier addFiles(MultipartFile MyFile, String Emeteur, String Recepteur, String NomFichier) throws IOException {
        Fichier fichier = new Fichier();

        // Save File
        fichier.setMyPath(saveFile(MyFile));

        // Generate Conversation Id
        String idConversation = generateConversationId(Emeteur, Recepteur);
        fichier.setIdConversation(idConversation);

        fichier.setEmeteur(Emeteur);
        fichier.setRecepteur(Recepteur);
        fichier.setNomFichier(NomFichier);
        Date date = new Date(); // replace this with your Date object
        LocalDateTime localDateTime = LocalDateTime.ofInstant(date.toInstant(), ZoneId.systemDefault());
        fichier.setDate(localDateTime);

        return fichierRepository.save(fichier);
    }

    private String generateConversationId(String Emeteur, String Recepteur) {
        // Concatenate Emeteur and Recepteur in alphabetical order
        return Stream.of(Emeteur, Recepteur)
                .sorted()
                .collect(Collectors.joining("_"));
    }


    private String saveFile(MultipartFile file) throws IOException {
        // Directory where files will be saved
        String uploadDirectory = "C:\\Users\\I511\\OneDrive\\Bureau\\FichierService\\src\\main\\resources\\fichiers";


        Path uploadPath = Path.of(uploadDirectory);
        Files.createDirectories(uploadPath);

        // smya unique
        String fileName = System.currentTimeMillis() + "_" + file.getOriginalFilename();


        Path filePath = uploadPath.resolve(fileName);


        Files.copy(file.getInputStream(), filePath, StandardCopyOption.REPLACE_EXISTING);


        return filePath.toString();
    }

    public Fichier getFichierById(String fichierId) {
        return fichierRepository.findById(fichierId).orElse(null);
    }

    public Optional<Fichier> getFichierByName(String nomFichier) {
        List<Fichier> fichiers = fichierRepository.findByNomFichier(nomFichier);
        if (!fichiers.isEmpty()) {
            return Optional.of(fichiers.get(0));
        } else {
            return Optional.empty();
        }
    }

    public List<Fichier> getFichiersbyConversation(String conversationId) {
        if (conversationId == null) {
            throw new IllegalArgumentException("Id post cannot be null");
        }

        List<Fichier> allFichier = fichierRepository.findAll(); // Obtenez tous les stages

        List<Fichier> Fichierbyconversation = new ArrayList<Fichier>();

        for (Fichier fichier: allFichier) {
            // Vérifiez si le stage spécifiée
            if (conversationId.equals(fichier.getIdConversation())) {
                Fichierbyconversation.add(fichier);
            }
        }

        return Fichierbyconversation;
    }
}
