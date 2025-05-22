using System;

public interface i_channel{
    void send_message(message msg);
}

public class whatsapp :i_channel {
    public string whatsapp_r {get; set; }
    public string whatsapp_destinatario { get; set;}

    public whatsapp(string whatsapp_r,string whatsapp_destinatario){
        this.whatsapp_r=whatsapp_r;
        this.whatsapp_destinatario = whatsapp_destinatario; //?
    }

    public void send_message(message msg) {
        Console.WriteLine($"\n[WhatsApp - De: {whatsapp_r} Para: {whatsapp_destinatario}]");
        msg.display_data();
    }
}

public class telegram:i_channel {
    public string meu_user { get; set;}
    public string my_tel {get; set; }
    public string user_destinatario { get; set; }
    public string telefone_destinatario { get; set; }

    public telegram(string my_tel,string meu_user, string telefone_destinatario,string user_destinatario) {
        this.my_tel = my_tel;
        this.meu_user=meu_user;
        this.telefone_destinatario = telefone_destinatario;
        this.user_destinatario=user_destinatario;
    }

    public void send_message(message msg){
        Console.WriteLine($"\n[Telegram - De: {meu_user} ({my_tel}) Para: {user_destinatario} ({telefone_destinatario})]");
        msg.display_data();
        // nm
    }
}

// fb
public class facebook : i_channel{
    public string my_face { get; set;}
    public string usuario_outro {get; set; }

    public facebook(string my_face,string usuario_outro){
        this.my_face = my_face;
        this.usuario_outro=usuario_outro;
    }

    public void send_message(message msg) {
        Console.WriteLine($"\n[Facebook - De: @{my_face} Para: @{usuario_outro}]");
        msg.display_data(); 
    }
}

public class instagram:i_channel {
    public string meu_usuario {get; set; }
    public string outro_usuario { get; set; }

    public instagram(string meu_usuario, string outro_usuario){
        this.meu_usuario=meu_usuario;
        this.outro_usuario = outro_usuario;
    }

    public void send_message(message msg){
        Console.WriteLine($"\n[Instagram - De: @{meu_usuario} Para: @{outro_usuario}]");
        msg.display_data(); //?
    }
}

public abstract class message {
    public string texto { get; set;}
    public DateTime data_envio {get; set; }

    public message(string texto){
        this.texto=texto;
        data_envio = DateTime.Now; //data
    }

    public abstract void display_data();
}

public class text_message:message{
    public text_message(string texto) : base(texto){
    }

    public override void display_data(){
        Console.WriteLine($"[Texto] {texto}");
        Console.WriteLine($"Enviado em {data_envio:dd/MM/yyyy HH:mm:ss}");
    }
}

public class video_message : message {
    public string arquivo {get; set; }
    public string formato { get; set;}
    public int duracao { get; set; }

    public video_message(string texto,string arquivo, string formato,int duracao):base(texto){
        this.arquivo = arquivo;
        this.formato=formato;
        this.duracao = duracao; 
    }

    public override void display_data() {
        Console.WriteLine($"[Vídeo] {texto} | Arquivo: {arquivo}, Formato: {formato}, Duração: {duracao}s");
        Console.WriteLine($"Enviado em {data_envio:dd/MM/yyyy HH:mm:ss}");
    }
}

public class photo_message: message{


    public string arquivo { get; set;}
    public string formato {get; set; }

    public photo_message(string texto, string arquivo,string formato) : base(texto){
        this.arquivo=arquivo;
        this.formato = formato; 
    }

    public override void display_data(){
        Console.WriteLine($"[Foto] {texto} | Arquivo: {arquivo}, Formato: {formato}");
        Console.WriteLine($"Enviado em {data_envio:dd/MM/yyyy HH:mm:ss}");
    }
}

public class file_message :message {

    public string arquivo {get; set; }
    public string formato { get; set; }

    public file_message(string texto,string arquivo, string formato) : base(texto) {
        this.arquivo = arquivo;
        this.formato=formato;
    }

    public override void display_data() {
        Console.WriteLine($"[Arquivo] {texto} | Arquivo: {arquivo}, Formato: {formato}");
        Console.WriteLine($"Enviado em {data_envio:dd/MM/yyyy HH:mm:ss}");
    }
}

class program {
    static void Main(string[] args) {
        i_channel canal=null;

        // loop canal
        while(canal==null){
            Console.WriteLine("Escolha o canal pra mandar a mensagem: ");
            Console.WriteLine("1. WhatsApp");
            Console.WriteLine("2. Telegram");
            Console.WriteLine("3. Facebook ");
            Console.WriteLine("4. Instagram");

            Console.Write("\nOpção:  ");
            string input=Console.ReadLine();
            if(!int.TryParse(input,out int opcao)||opcao<1||opcao>4){
                Console.WriteLine("\nOpção inválida, escolha novamente\n");
                continue;
            }

            switch(opcao){
                case 1:
                    Console.Write("\nSeu telefone:  ");
                    string whatsapp_r=Console.ReadLine();
                    Console.Write("\nTelefone do destinatário: ");
                    string whatsapp_destinatario = Console.ReadLine();
                    canal=new whatsapp(whatsapp_r,whatsapp_destinatario);
                    break;

                case 2:
                    Console.Write("\nSeu número de telefone: ");
                    string my_tel = Console.ReadLine();
                    Console.Write("\nSeu usuario: ");
                    string usuario_meu=Console.ReadLine();
                    Console.Write("\n\nTelefone do destinatario: ");
                    string telefone_destinatario = Console.ReadLine();
                    Console.Write("\nUsuario do destinatario: ");
                    string usuario_outro=Console.ReadLine();
                    canal = new telegram(my_tel,usuario_meu,telefone_destinatario,usuario_outro);
                    break;

                case 3:
                    Console.Write("\nSeu usuario: ");
                    string my_face=Console.ReadLine();
                    Console.Write("\nUsuario do destinatario: ");
                    string face_outro = Console.ReadLine();
                    canal=new facebook(my_face,face_outro);
                    break;

                case 4:
                    Console.Write("\nSeu usuario: ");
                    string insta_meu = Console.ReadLine();
                    Console.Write("\nUsuario do destinatario: ");
                    string insta_outro=Console.ReadLine();
                    canal = new instagram(insta_meu,insta_outro);
                    break;
            }
        }

        // loop msg
        while(true){
            Console.WriteLine("\nEscolha o tipo de mensagem: ");
            Console.WriteLine("1. Texto");
            Console.WriteLine("2. Vídeo");
            Console.WriteLine("3. Foto ");
            Console.WriteLine("4. Arquivo");

            Console.Write("\nOpção: ");
            int tipo;
            if(!int.TryParse(Console.ReadLine(),out tipo)){
                Console.WriteLine("Opção inválida, escolha um número.");
                continue;
            }

            if(tipo==1){
                while(true){
                    Console.Write("\nTexto da mensagem (digite 'quit' pra sair): ");
                    string texto=Console.ReadLine();
                    if(texto.ToLower()=="quit"){
                        Console.WriteLine("Saindo...");
                        return; 
                    }
                    message mensagem=new text_message(texto);
                    canal.send_message(mensagem);
                    Console.WriteLine();
                }
            }
            else{
                message mensagem=null;
                string texto = "";

                if(tipo==2){
                    Console.Write("\nArquivo de video (simulado): ");
                    string video_arquivo=Console.ReadLine();
                    Console.Write("Formato do video: ");
                    string formato_video = Console.ReadLine();
                    Console.Write("Duração (em segundos): ");
                    int duracao;
                    if(!int.TryParse(Console.ReadLine(),out duracao)){
                        Console.WriteLine("Duração inválida, tente novamente.");
                        continue; //verificar
                    }
                    mensagem=new video_message(texto,video_arquivo,formato_video,duracao);
                }
                else if(tipo==3){
                    Console.Write("\nArquivo de foto (simulado): ");
                    string foto_arquivo = Console.ReadLine();
                    Console.Write("Formato da foto: ");
                    string formato_foto=Console.ReadLine();
                    mensagem = new photo_message(texto,foto_arquivo,formato_foto);
                }
                else if(tipo==4){
                    Console.Write("\nArquivo (simulado): ");
                    string arquivo=Console.ReadLine();
                    Console.Write("Formato do arquivo: ");
                    string formato_arquivo = Console.ReadLine();
                    mensagem=new file_message(texto,arquivo,formato_arquivo);
                }
                else{
                    Console.WriteLine("Não é uma opção.");
                    return;
                }

                canal.send_message(mensagem);
                Console.WriteLine(); //?
            }
        }
    }
}
